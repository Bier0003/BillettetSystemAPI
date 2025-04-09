
using ModelBilletterSystem;

namespace BillettetSystemAPI.Interfaces
{
    public interface ICategory
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int Id_Category);
        Task<Category> CreateCategory(string category_name);
        Task<Category> UpdateCategory(int Id_Category, string category_name);
        Task<bool> DeleteCategory(int Id_Category);
    }
}
