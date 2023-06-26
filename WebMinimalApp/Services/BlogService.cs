using WebMinimalApp.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebMinimalApp.Services
{
    public class BlogService : IBlogService
    {
        private readonly List<Blog> _blogList;
        public BlogService() {
            _blogList = new List<Blog>();
        }
        public Blog Create(Blog blog)
        {
            blog.Id = GenerateNewId();
            _blogList.Add(blog);
            return blog;
        }

        public void Delete(int id)
        {
            var todoItem = _blogList.FirstOrDefault(item => item.Id == id);
            if (todoItem != null)
            {
                _blogList.Remove(todoItem);
            }
        }

        public IEnumerable<Blog> GetAll()
        {
            return _blogList;
        }

        public Blog GetById(int id)
        {
           return _blogList.FirstOrDefault(item => item.Id == id);
        }

        public void Update(int id, Blog blog)  {
            var existingTodoItem = _blogList.FirstOrDefault(item => item.Id == id);
            if (existingTodoItem != null)
            {
                existingTodoItem.Title = blog.Title;
                existingTodoItem.Description = blog.Description;
            }

        }

        private int GenerateNewId()
        {
            if (_blogList.Count > 0)
            {
                return _blogList.Max(item => item.Id) + 1;
            }
            return 1;
        }
    }
}
