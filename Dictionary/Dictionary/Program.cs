Welcome();

string dictionaryFile = "dictionary.txt";
Dictionary<string, string> dictionary = new Dictionary<string, string>();

CheckDictionaryFile();
LoadDictionaryFile();

while ( true )
{
    Console.Write( "Write a word to translate: " );
    string userWord = Console.ReadLine().ToLower();

    while ( string.IsNullOrWhiteSpace( userWord ) )
    {
        Console.Write( "Write a word to translate: " );
        userWord = Console.ReadLine().ToLower();
    }

    string translateWord = TranslateWord( userWord );

    if ( translateWord != null )
    {
        Console.WriteLine( $"Translated word: {translateWord}" );
    }
    else
    {
        WordNotFound( userWord );
    }
}

void Welcome()
{
    Console.WriteLine( "Dictionary" ); //Приветственное сообщение
}

void LoadDictionaryFile()
{
    foreach ( string line in File.ReadAllLines( dictionaryFile ) )
    {
        string[] word = line.Split( '=' );
        dictionary[ word[ 0 ].Trim() ] = word[ 1 ].Trim();
    }
}

void CheckDictionaryFile()
{
    if ( !File.Exists( dictionaryFile ) )
    {
        using ( File.Create( dictionaryFile ) ) {}

        Console.WriteLine( "Create dictionary" ); //Словарь создался в папке bin/Debug/net8.0
    }
}

string TranslateWord( string word )
{
    if ( dictionary.TryGetValue( word, out string result ) )
    {
        return result; //Слово найдено в словаре
    }

    return null; //Слово не найдено в словаре
}

void AddNewWord( string word )
{
    Console.Write( "Write translate to this word: " );
    string userWordTranslate = Console.ReadLine().ToLower();

    while ( string.IsNullOrWhiteSpace( userWordTranslate ) )
    {
        Console.Write( "Write translate this word: " );
        userWordTranslate = Console.ReadLine().ToLower();
    }

    using ( StreamWriter file = new StreamWriter( dictionaryFile, append: true ) )

    {
        file.WriteLine( $"{word}={userWordTranslate}" );
        dictionary[ word ] = userWordTranslate;
    }

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

    string userCommand = Console.ReadLine();

    switch ( userCommand )
    {
        case "1":
            AddNewWord( word );
            break;
        case "2":
            break;
        default:
            Console.WriteLine( "Unknown command" );
            break;
    }
}