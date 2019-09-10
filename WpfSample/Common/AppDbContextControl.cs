using System;
using WpfSample.Models;
using static WpfSample.Common.DB;

namespace WpfSample.Common
{
    public class AppDbContextControl
    {
        public static void ExecSaveChanges(Action action)
        {
            using (AppDbContext contexForSave = CreateAppDbContextForSave(true))
            {
                action();
                contexForSave.SaveChanges();
            }
        }
    }
}
