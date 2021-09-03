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
            if (GUILayout.Button("�Ыةж�")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("�[�J�ж�")) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("�Ыئ��A��")) NetworkManager.Singleton.StartServer();
        }

        private static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ? "�Ыةж�" : NetworkManager.Singleton.IsServer ? "�Ыئ��A��" : "�[�J�ж�";

            GUILayout.Label("�ǿ�G" + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("�Ҧ��G" + mode);
        }

        private static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "����" : "�n�D���ܮy��"))
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
