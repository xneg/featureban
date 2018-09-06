namespace Featureban.Domain
{
    public class Token
    {
        private readonly TokenType _tokenType;

        public bool IsTails => _tokenType == TokenType.Tails;

        public bool IsEagle => _tokenType == TokenType.Eagle;

        public Token(TokenType tokenType)
        {
            _tokenType = tokenType;
        }        
    }

    public enum TokenType
    {
        Eagle = 0,

        Tails = 1
    }
}