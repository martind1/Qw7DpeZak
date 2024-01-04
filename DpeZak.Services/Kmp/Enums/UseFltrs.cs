namespace DpeZak.Portal.Services.Kmp
{


    public partial class DbLink<TItem> where TItem : class
    {
        [Flags]
        public enum UseFltrs
        {
            UseAll = 0,
            UseFltrlist = 1,
            UseReferences = 2
        }




    }


}
