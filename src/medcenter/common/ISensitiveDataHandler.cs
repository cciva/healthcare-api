using System;
using System.IO;

namespace MedCenter
{
    // this interface is meant to be used as encryption
    // abstraction for managing our secrets and sensitive data
    // WARNING : THIS IS JUST FOR ASSESMENT DEMONSTRATION
    // PURPOSES ONLY. DO NOT USE IN REAL-WORLD SCENARIOS
    public interface ISensitiveDataHandler
    {
        string Encrypt(Stream stream);
        string Encrypt(string str);

        string Decrypt(Stream stream);
        string Decrypt(string input);
    }
}