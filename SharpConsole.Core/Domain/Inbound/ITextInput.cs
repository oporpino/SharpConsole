namespace SharpConsole.Core.Domain.Inbound;

public interface ITextInput
{
  string Text { get; }
  int Position { get; }
}
