using UnityEngine;
using MLAPI;

namespace KID
{
    public class GameManager : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        private static void StartButtons()
        {
            if (GUILayout.Button("創建房間")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("加入房間")) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("創建伺服器")) NetworkManager.Singleton.StartServer();
        }

        private static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ? "創建房間" : NetworkManager.Singleton.IsServer ? "創建伺服器" : "加入房間";

            GUILayout.Label("傳輸：" + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("模式：" + mode);
        }

        private static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "移動" : "要求改變座標"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkClient))
                {
                    var player = networkClient.PlayerObject.GetComponent<Player>();
                    if (player) player.Move();
                }
            }
        }
    }
}
