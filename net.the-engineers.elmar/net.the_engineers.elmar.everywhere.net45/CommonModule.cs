using SimpleInjector;

namespace net.the_engineers.elmar.everywhere.net45
{
    public class CommonModule
    {
        public static void Register(Container container)
        {
            container.RegisterSingle<IGuidGenerator, GuidGenerator>();
            container.RegisterSingle<IDateTimeProvider, SystemDateTimeProvider>();
            ;
        }
    }
}