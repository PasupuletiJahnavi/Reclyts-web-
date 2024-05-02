using Loginregister.Models;
using Microsoft.EntityFrameworkCore;
namespace Loginregister.ViewModels

{
    public class DashboardViewModel
    {
        public IEnumerable<TaskModel> Tasks { get; set; }
        public IEnumerable<Document> Documents { get; set; }
        public IEnumerable<Tutorial> Tutorials { get; set; }
        public IEnumerable<UpdateInfo> UpdateInfos { get; set; }

        public List<IEnumerable<Comment>> Comments { get; set; }

        public DashboardViewModel(int numberOfSections)
        {
            Comments = new List<IEnumerable<Comment>>(numberOfSections);

            for (int i = 0; i < numberOfSections; i++)
            {
                Comments.Add(new List<Comment>());
            }
        }
    }
}
