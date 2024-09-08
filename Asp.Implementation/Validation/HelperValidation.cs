using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.DataAccess;

namespace Asp.Implementation.Validation
{
    public static class HelperValidation
    {

        public static bool CategoryExists(int id, AspDbContext context)
        {
            return context.Categories.Any(x => x.Id == id && x.IsActive == true);
        }

        public static bool CategoryNotInUse(string categoryName, AspDbContext context)
        {
            return !context.Categories.Any(x => x.CategoryName == categoryName);
        }
        public static bool UserExists(int? userId, AspDbContext context)
        {
            return context.Users.Any(x => x.Id == userId);
        }

        public static bool AlreadyFriends(int? requestId, int? acceptId, AspDbContext context, bool negate = false)
        {
            var result = !context.UserFriends.Any(x => x.RequestUserId == requestId && x.AcceptUserId == acceptId 
            || x.AcceptUserId == requestId && x.RequestUserId == acceptId );
            return negate ? !result : result;
        }
        public static bool PostExists(int id, AspDbContext context)
        {
            return context.Posts.Any(x => x.Id == id && x.IsActive == true);
        }

        public static bool CommentExist(int? id, AspDbContext context)
        {
            return context.Comments.Any(x => x.Id == id && x.IsActive == true);
        }
        public static bool TitleNotInUse(string title, int id, AspDbContext context)
        {
            return !context.Posts.Any(x => x.Title == title && x.IsActive == true && x.UserId == id);
        }


        public static bool CategoriesExist(IEnumerable<int> ids, AspDbContext context)
        {
            foreach (var id in ids)
            {
                if (!context.Categories.Any(x => x.Id == id && x.IsActive == true))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
