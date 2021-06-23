namespace PLX.API.Data.DTO
{
    public class ListItem
    {
        public ListItem() { }
        public ListItem(string value, string display)
        {
            Value = value;
            Display = display;
        }
        public string Value { get; set; }
        public string Display { get; set; }
    }
}