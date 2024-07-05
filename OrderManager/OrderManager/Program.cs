PrintApplicationName();
DateTime orderDate = DateTime.Now.AddDays( 3 );

while ( true )
{
    string userProduct = ReadProduct();
    decimal productCount = ReadCount();
    string userName = ReadUsername();
    string userAddress = ReadAddress();

    Console.WriteLine( $"{userName}, You have ordered {productCount} {userProduct} for {userAddress}, is that correct? " );
    PrintAnswer();

    string userCommand = Console.ReadLine();
    switch ( userCommand )
    {
        case "Yes":
            Console.WriteLine( $"{userName}, your order {userProduct} in quantity {productCount} has been placed! Expect delivery to {userAddress} by {orderDate}" );
            return;
        case "No":
            break;
        default:
            Console.WriteLine( "Unknown command" );
            break;
    }
}

static void PrintApplicationName()
{
    Console.WriteLine( "You are welcome to our store!" );
}

static string ReadProduct()
{
    Console.Write( "What do you want to order: " );
    string product = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( product ) )
    {
        Console.Write( "Please, enter the item you wish to order: " );
        product = Console.ReadLine();
    }

    return product;
}

static decimal ReadCount()
{
    Console.Write( "How much do you want: " );
    decimal count;
    while ( !decimal.TryParse( Console.ReadLine(), out count ) )
    {
        Console.Write( "You have entered an incorrect value for the quantity, please enter a number: " );
    }

    return count;
}

static string ReadUsername()
{
    Console.Write( "Write your name: " );
    string userName = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( userName ) )
    {
        Console.Write( "Write your name: " );
        userName = Console.ReadLine();
    }

    return userName;
}

static string ReadAddress()
{
    Console.Write( "What address do you want your order delivered to: " );
    string address = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( address ) )
    {
        Console.Write( "What address do you want your order delivered to: " );
        address = Console.ReadLine();
    }

    return address;
}

void PrintAnswer()
{
    Console.WriteLine( "[Yes]" );