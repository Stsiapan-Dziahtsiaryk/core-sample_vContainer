namespace Gameplay.Presentation.Data
{
    public readonly struct CardDto
    {
        public readonly int ID;
        public readonly int CardWeight;

        public CardDto(int id, int cardWeight)
        {
            ID = id;
            CardWeight = cardWeight;
        }
    }
}