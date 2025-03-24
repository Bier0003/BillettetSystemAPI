using ModelBilletterSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BillettetSystemAPI.Interfaces
{
    public interface ICategory
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(string category_name);
        Task<Category> UpdateCategory(int id, string category_name);
        Task<bool> DeleteCategory(int id);
    }
}
