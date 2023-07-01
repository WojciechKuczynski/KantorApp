namespace KantorClient.Application.Models
{
    public class ComboBoxItem
    {
        public bool Selected { get; set; }
        public object Object { get; set; }

        public override string ToString()
        {
            return this.Object.ToString();
        }
    }
}
