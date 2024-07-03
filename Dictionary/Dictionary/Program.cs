Welcome();

string dictionaryFile = "dictionary.txt";
CheckDictionaryFile();
Dictionary<string, string> dictionary = new Dictionary<string, string>();

while ( true )
{
    Console.Write( "Write a word to translate: " );
    string userWord = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( userWord ) )
    {
        Console.Write( "Write a word to translate: " );
        userWord = Console.ReadLine();
    }
    string translateWord = TranslateWord( userWord );
    if ( translateWord != null )
    {
        Console.WriteLine( $"Translated word: {translateWord}" );
    }
    else
    {
        Console.WriteLine( "That word is not found in the dictionary, would you like to add it?" );
        PrintMenu();
        string userCommand = Console.ReadLine();
        switch ( userCommand )
        {
            case "1":
                AddNewWord( userWord );
                break;
            case "2":
                break;
            default:
                Console.WriteLine( "Unknown command" );
                break;
        }
    }
}

void Welcome()
{
    Console.WriteLine( "Dictionary" );
}

void CheckDictionaryFile()
{
    if ( !File.Exists( dictionaryFile ) )
    {
        using ( File.Create( dictionaryFile ) )
        {

        }
        Console.WriteLine( "Create dictionary" ); //Словарь создался в папке bin/Debug/net8.0
    }
}

string TranslateWord( string word )
{
    using ( StreamReader file = new StreamReader( dictionaryFile ) )
    {
        string line;
        while ( ( line = file.ReadLine() ) != null )
        {
            string[] parts = line.Split( '=' ); //Делим строку на ключ и значение
            string leftWord = parts[ 0 ];
            string rightWord = parts[ 1 ];
            dictionary[ leftWord ] = rightWord;
            if ( leftWord == word ) //Ищем наше слово
            {
                return rightWord;
            }
            else if ( rightWord == word )
            {
                return leftWord;
            }
        }
    }
    return null; //Слово не найдено
}

void AddNewWord( string word )
{
    Console.Write( "Write translate to this word: " );
    string userWordTranslate = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( userWordTranslate ) )
    {
        Console.Write( "Write translate this word: " );
        userWordTranslate = Console.ReadLine();
    }
    using ( StreamWriter file = new StreamWriter( dictionaryFile, append: true ) )
    {
        file.WriteLine( $"{word}={userWordTranslate}" );
    }
    Console.WriteLine( "New word added" );
}

void PrintMenu()
{
    Console.WriteLine( "[1] Add word in dictionary" );
    Console.WriteLine( "[2] Not add" );
}