namespace Featureban.Domain
{
    public class Token
    {
        private enum TokenType
        {
            Eagle = 0,

            Tails = 1
        }
        private readonly TokenType _tokenType;

        public bool IsTails => _tokenType == TokenType.Tails;

        public bool IsEagle => _tokenType == TokenType.Eagle;

        private Token(TokenType tokenType)
        {
            _tokenType = tokenType;
        }        
        public static Token CreateTailsToken()
        {
            return new Token(TokenType.Tails);
        }
        public static Token CreateEagleToken()
        {
            return new Token(TokenType.Eagle);
        }
    }

   
}