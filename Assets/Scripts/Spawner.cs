using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Block[] _blocks = null;

    Block GetRandomBlock()
    {
        int i = Random.Range(0, _blocks.Length);

        if (_blocks[i])
        {
            return _blocks[i];
        }
        else
        {
            return null;
        }
    }

    public Block SpawnBlock()
    {
        Block block = Instantiate(GetRandomBlock(), transform.position, Quaternion.identity);

        if (block)
        {
            return block;
        }
        else
        {
            return null;
        }
    }
}
