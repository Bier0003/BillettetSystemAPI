
using BillettetSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelBilletterSystem;

namespace BillettetSystemAPI.Repositories
{
    public class CategoryRepository : ICategory
    {

        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public Task<Category> CreateCategory(string category_name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryById(int Id_Category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(int id, string category_name)
        {
            throw new NotImplementedException();
        }

       
    }
}
