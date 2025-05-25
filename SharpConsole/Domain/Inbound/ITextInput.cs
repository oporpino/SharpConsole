namespace SharpConsoleCore.Domain.Inbound;

public interface ITextInput
{
  string Text { get; }
  int Position { get; }
}
