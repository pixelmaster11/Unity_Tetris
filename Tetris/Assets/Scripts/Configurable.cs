
using Configs;

/// <summary>
/// Base class to enforce acceptance of a config file
/// Used by Board, Tetromino manager, Input manager to accept their own config files
/// </summary>
public abstract class Configurable 
{
    public Configurable(BaseConfig _config) {}
     
}
