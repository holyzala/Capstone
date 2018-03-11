// In order of power
public enum Suit
{
    Diamond, Hearts, Spades, Clubs
};

// In order of power
public enum CardID
{
    Seven, Eight, Nine, King, Ten, Ace, Jack, Queen
};

public enum CardPower
{
    SevenFail = 1, EightFail, NineFail, KingFail, TenFail, AceFail, SevenTrump,
    EightTrump, NineTrump, KingTrump, TenTrump, AceTrump, JackDiamond,
    JackHeart, JackSpade, JackClub, QueenDiamond, QueenHeart, QueenSpade,
    QueenClub
};

// This enum will contain the different prompt types that can be called
public enum PromptType
{
    Pick, PlayCard, PickBlind, RoundOver, GameOver, TableOver, CallUp, CalledUp
}

public enum PromptData
{
    Player,  // IPlayer
    Picker,  // IPlayer
    Blind,  // IBlind
    Trick,  // ITrick
    Scores,  // IScoreSheet
    Round,  // IRound
    Rounds,  // List<IRound>
    Game,  // IGame
    Games,  // List<IGame>
    Card,  // ICard
    Cards  // List<ICard>
}