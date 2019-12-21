using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Core.Logging;
using DAL;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;

namespace Core.Repositories
{
    public interface IPublicationRepository
    {
        Task<IReadOnlyCollection<Publication>> GetPublications(int? categoryId, int languageId, int page, int pageSize);
        Task<int> GetPublicationsCount(int? categoryId, int languageId);
        Task<IReadOnlyCollection<Category>> GetCategories();
        Task<Publication> GetPublication(int id);
        Task<Publication> Save(Publication publication);
        Task IncreasePublicationViewCount(int id);
        Task<Publication> GetPublication(Uri uri);
        Task<IReadOnlyCollection<string>> GetCategoryTags(int categoryId);
        Task<IReadOnlyCollection<Publication>> GetTopPublications(int languageId);
    }

    public class PublicationRepository : IPublicationRepository
    {
        private readonly DatabaseContext _database;
        private readonly ILogger _logger;

        public PublicationRepository(DatabaseContext database, ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Publication>> GetPublications(int? categoryId, int languageId, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            return await _database
                .Publication
                .Where(o => o.CategoryId == categoryId || categoryId == null)
                .Where(o => o.LanguageId == languageId)
                .OrderByDescending(o => o.DateTime)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<int> GetPublicationsCount(int? categoryId, int languageId) =>
            _database.Publication
                .Where(o => o.CategoryId == categoryId || categoryId == null)
                .Where(o => o.LanguageId == languageId)
                .CountAsync();

        public async Task<IReadOnlyCollection<Category>> GetCategories() =>
            await _database.Category.ToListAsync();

        public Task<Publication> GetPublication(int id)
        {
            return _database.Publication.SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Publication> Save(Publication publication)
        {

            _database.Add(publication);
            await _database.SaveChangesAsync();

            publication = await _database.Publication.OrderBy(o => o.DateTime).LastOrDefaultAsync();

            _logger.Write(LogEventLevel.Information, $"Publication `{publication.Title}`  was saved. Id: {publication.Id}");

            return publication;
        }

        public async Task IncreasePublicationViewCount(int id)
        {
            var publication = _database.Publication.SingleOrDefault(o => o.Id == id);

            if (publication != null)
            {
                publication.Views++;
                await _database.SaveChangesAsync();
            }
        }

        public Task<Publication> GetPublication(Uri uri) =>
            _database.Publication.SingleOrDefaultAsync(o => o.Link.ToLower() == uri.ToString().ToLower());

        public async Task<IReadOnlyCollection<string>> GetCategoryTags(int categoryId)
        {
            var value = await _database.Category
                .Where(o => o.Id == categoryId)
                .Select(o => o.Tags)
                .SingleOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(value))
                return ImmutableArray<string>.Empty;

            return value.Split(' ').ToImmutableList();
        }

        public async Task<IReadOnlyCollection<Publication>> GetTopPublications(int languageId)
        {
            var categories = _database.Category.Select(o => o.Id).ToImmutableHashSet();

            var publications = await _database.Publication
                .Where(p => p.LanguageId == languageId)
                .Where(p => p.DateTime > DateTime.Now.AddDays(-30))
                .ToListAsync();

            return publications
                .GroupBy(p => p.CategoryId)
                .Select(g => g.FirstOrDefault())
                .ToImmutableList();
        }
    }
}