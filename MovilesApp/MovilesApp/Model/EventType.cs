namespace MovilesApp.Model
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Buscar la manera de usar imagenes del sistema
        //      Bitmap?
        //public int ImageResource { get; set; }

        public string HexColor { get; set; }

        public EventType() { }

        public EventType(int id) { this.Id = id; }

        public EventType(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public EventType(int id, string name, string hexColor)
        {
            this.Id = id;
            this.Name = name;
            this.HexColor = hexColor;
        }
    }
}