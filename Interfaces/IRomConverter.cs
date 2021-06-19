using System;
namespace RomConverter
{
    public interface IRomConverter
    {
        bool Convert(string inputPath, string outputPath);
    }
}
