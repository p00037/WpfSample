using WpfSample.Models;

namespace WpfSample.Common
{
    public class DB
    {
        private static AppDbContext context;
        private static AppDbContext contextForSave;

        public static AppDbContext CreateAppDbContext()
        {
            if (context != null) context.Dispose();
            context = new AppDbContext();
            return context;
        }

        public static AppDbContext CreateAppDbContextForSave(bool initialize)
        {
            if (contextForSave == null || initialize == true) contextForSave = new AppDbContext();
            return contextForSave;
        }

        public static AppDbContext CreateAppDbContextForSave()
        {
           return CreateAppDbContextForSave(false);
        }
    }
}
