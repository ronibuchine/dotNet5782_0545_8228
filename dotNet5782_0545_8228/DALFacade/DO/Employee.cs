
namespace DO
{
    public class Employee : DalEntity
    {
        public string password { get; set; }

        public Employee(int ID, string password) : base(ID)
        {
            this.password = password;
        }
        public override Employee Clone() => this.MemberwiseClone() as Employee;

        public override string ToString()
        {
            return $"Employee ID = {ID}";
        }
    }
}
