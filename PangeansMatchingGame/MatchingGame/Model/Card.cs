namespace MatchingGame.Model
{
    public class Card : ICloneable
    {
        public int Code { get; private set; }
        public string Name { get; private set; }
        public string[] Value { get; private set; }
        public CardState State { get; set; }
        public Card(int code, string[] value, string name)
        {
            Code = code;
            Value = value;
            Name = name;
            State = CardState.Guessing;
        }

        public override string ToString()
        {
            return Name;
        }

        public object Clone()
        {
            var clonedCard = (Card)this.MemberwiseClone();
            clonedCard.Value = (string[])this.Value.Clone();
            return clonedCard;
        }
    }
}
