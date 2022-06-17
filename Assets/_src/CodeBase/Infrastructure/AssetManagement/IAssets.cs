using _src.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}