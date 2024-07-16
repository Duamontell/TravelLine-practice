Console.WriteLine( "Dictionary" );

string dictionaryFile = "dictionary.txt";
Dictionary<string, string> initDictionary = new Dictionary<string, string>();
Dictionary<string, string> dictionary = new Dictionary<string, string>();

LoadDictionaryFile();

while ( true )
{
    PrintMenu();

    string userCommand = Console.ReadLine().Trim();

    switch ( userCommand )
    {
        case "1":
            TranslateWord();
            break;
        case "2":
            AddNewWord();
            break;
        case "3":
            SaveDictionary();
            return;
    }
}

void PrintMenu()
{
    Console.WriteLine( "[1] Translate word" );
    Console.WriteLine( "[2] Add new word to dictionary" );
    Console.WriteLine( "[3] Exit" );
}

void LoadDictionaryFile()
{
    if ( !File.Exists( dictionaryFile ) )
    {
        using ( File.Create( dictionaryFile ) )
        { }
        Console.WriteLine( "Create dictionary" );
    }

    foreach ( string line in File.ReadAllLines( dictionaryFile ) )
    {
        string[] word = line.Split( '=' );
        dictionary[ word[ 0 ] ] = word[ 1 ];
    }
}

void AddNewWord()
{
    Console.Write( "Write a word for translate: " );
    string userWord = Console.ReadLine().ToLower().Trim();

    while ( string.IsNullOrWhiteSpace( userWord ) || IsString( userWord ) )
    {
        Console.Write( "Invalid input, write a word for translate: " );
        userWord = Console.ReadLine().ToLower().Trim();
    }

    Console.Write( "Write translate to this word: " );
    string userWordTranslate = Console.ReadLine().ToLower().Trim();

    while ( string.IsNullOrWhiteSpace( userWordTranslate ) || IsString( userWordTranslate ) )
    {
        Console.Write( "Invalid input, write translate this word: " );
        userWordTranslate = Console.ReadLine().ToLower().Trim();
    }

    initDictionary[ userWord ] = userWordTranslate;
    dictionary[ userWord ] = userWordTranslate;
    Console.WriteLine( "New word added" );
}

void TranslateWord()
{
    Console.Write( "Write a word to translate: " );
    string userWord = Console.ReadLine().ToLower().Trim();

    while ( string.IsNullOrWhiteSpace( userWord ) || IsString( userWord ) )
    {
        Console.Write( "Invalid input, write a word to translate: " );
        userWord = Console.ReadLine().ToLower().Trim();
    }

    if ( dictionary.TryGetValue( userWord, out string result ) )
    {
        Console.WriteLine( $"Translated word: {result}" );
        Console.WriteLine();
    }
    else
    {
        WordNotFound( userWord );
    }
}

void AddTranslateWord( string word )
{
    Console.Write( "Write translate to this word: " );
    string userWordTranslate = Console.ReadLine().ToLower().Trim();

    while ( string.IsNullOrWhiteSpace( userWordTranslate ) || IsString( userWordTranslate ) )
    {
        Console.Write( "Invalid input, write translate this word: " );
        userWordTranslate = Console.ReadLine().ToLower().Trim();
    }

    initDictionary[ word ] = userWordTranslate;
    dictionary[ word ] = userWordTranslate;
    Console.WriteLine( "New word added" );
}

void PrintAddMenu()
{
    Console.WriteLine( "[1] Add word in dictionary" );
    Console.WriteLine( "[2] Not add" );
}

void WordNotFound( string word )
{
    Console.WriteLine( "That word is not found in the dictionary, would you like to add it?" );
    PrintAddMenu();

    string userCommand = Console.ReadLine().Trim();

    switch ( userCommand )
    {
        case "1":
            AddTranslateWord( word );
            break;
        case "2":
            Console.WriteLine();
            break;
        default:
            Console.WriteLine( "Unknown command" );
            break;
    }
}

bool IsString( string word )
{
    if ( int.TryParse( word, out _ ) )
    {
        return true;
    }
    return false;
}

void SaveDictionary()
{
    if ( initDictionary.Count != 0 )
    {
        using ( StreamWriter file = File.AppendText( dictionaryFile ) )
        {
            foreach ( KeyValuePair<string, string> word in initDictionary )
            {
                file.WriteLine( $"{word.Key}={word.Value}" );
            }
        }
    }
}