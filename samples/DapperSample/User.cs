using Dapper.Contrib.Extensions;

namespace DapperSample
{
    [Table("tb_user")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
