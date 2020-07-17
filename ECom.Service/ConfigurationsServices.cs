using ECom.Database;
using ECom.Entities;

namespace ECom.Services.cs
{
    public class ConfigurationsServices
    {
        public static ConfigurationsServices Instence
        {
            get
            {
                if (instence == null)
                {
                    instence = new ConfigurationsServices();
                }
                return instence;
            }
        }

        private static ConfigurationsServices instence
        { get; set; }

        private ConfigurationsServices()
        {

        }

        public Config GetConfig(string Key)
        {
            using (var Context = new CDbContext())
            {
                return Context.Configurations.Find(Key);
            }
        }

    }
}
