// In order of power
using System.Runtime.Serialization;

public enum Suit
{
    [EnumMember(Value = "Diamonds")]
    Diamond,
    [EnumMember(Value = "Heart")]
    Hearts,
    [EnumMember(Value = "Spades")]
    Spades,
    [EnumMember(Value = "Clubs")]
    Clubs
};

// In order of power
public enum CardID
{
    [EnumMember(Value = "7")]
    Seven,
    [EnumMember(Value = "8")]
    Eight,
    [EnumMember(Value = "9")]
    Nine,
    [EnumMember(Value = "K")]
    King,
    [EnumMember(Value = "10")]
    Ten,
    [EnumMember(Value = "A")]
    Ace,
    [EnumMember(Value = "J")]
    Jack,
    [EnumMember(Value = "Q")]
    Queen
};
/*
  "Card_ID": 1,
  "Face": "7",
  "Suit": "Hearts",
  "is_Trump": false,
  "Trump_Power": 1,
  "Card_Value": 0
  */
public enum CardPower
{
    [EnumMember(Value = "1")]
    SevenFail = 1,
    [EnumMember(Value = "2")]
    EightFail,
    [EnumMember(Value = "3")]
    NineFail,
    [EnumMember(Value = "4")]
    KingFail,
    [EnumMember(Value = "5")]
    TenFail,
    [EnumMember(Value = "6")]
    AceFail,
    [EnumMember(Value = "7")]
    SevenTrump,
    [EnumMember(Value = "8")]
    EightTrump,
    [EnumMember(Value = "9")]
    NineTrump,
    [EnumMember(Value = "10")]
    KingTrump,
    [EnumMember(Value = "11")]
    TenTrump, AceTrump,
    [EnumMember(Value = "12")]
    JackDiamond,
    [EnumMember(Value = "13")]
    JackHeart,
    [EnumMember(Value = "14")]
    JackSpade,
    [EnumMember(Value = "15")]
    JackClub,
    [EnumMember(Value = "16")]
    QueenDiamond,
    [EnumMember(Value = "17")]
    QueenHeart,
    [EnumMember(Value = "18")]
    QueenSpade,
    [EnumMember(Value = "19")]
    QueenClub
};

// This enum will contain the different prompt types that can be called
public enum PromptType
{
    Pick, PlayCard, PickBlind, RoundOver, GameOver, TableOver, CallUp, CalledUp, CardsDealt, BotPlayCard
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
    Cards,  // List<ICard>
    Players  // List<IPlayer>
}