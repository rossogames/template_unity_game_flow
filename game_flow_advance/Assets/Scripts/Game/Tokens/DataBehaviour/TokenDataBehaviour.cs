using UnityEngine;

namespace RossoGames.Tokens.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(TokenDataBehaviour), menuName = "RossoGames/Data Behaviour/Token")]
    public class TokenDataBehaviour : ScriptableObject
    {
        [field: SerializeField]
        public float MoveSpeed { get; private set; }
    }
}
