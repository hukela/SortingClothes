using System.Data.SqlClient;

namespace SQLdata
{
    public class Config
    {
        static protected readonly SqlConnection con = new SqlConnection("server=DESKTOP-64OFAIT;database=ClothesServer;user=sa;pwd=123;");
        static protected readonly string path = "D:/www/SortingClothes/SortingClothes";
    }
}