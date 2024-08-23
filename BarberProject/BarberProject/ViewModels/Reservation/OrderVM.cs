namespace BarberProject.ViewModels.Reservation
{
    public class OrderVM
    {
        public string UserId { get; set; }
        public int EmployeeId { get; set; }  
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }

    }
}
