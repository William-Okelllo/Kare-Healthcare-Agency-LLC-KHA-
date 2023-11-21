using IShop.Core;
using IShop.Core.Interface;
using System.Collections;
using System.Linq;

namespace Ishop.Infa
{
    public class Project_Repo : Iproject
    {
        Project_Context context = new Project_Context();

        public void Add(Project project)
        {
            context.projects.Add(project);
            context.SaveChanges();
        }

        public void Edit(Project project)
        {
            context.Entry(project).State = System.Data.Entity.EntityState.Modified;
        }

        public Project FindById(int Id)
        {
            var result = (from r in context.projects where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetProject()

        { return context.projects; }

        public void Remove(int Id)

        {
            Project d = context.projects.Find(Id);
            context.projects.Remove(d); context.SaveChanges();

        }
    }
}
