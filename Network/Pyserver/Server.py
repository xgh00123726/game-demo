import socket

host = ''
port = 8888
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind((host, port))
server_socket.listen(5)

try:
    while True:
        client_socket, client_address = server_socket.accept()
        print(f"Accept connect from {client_address}")
        
finally:
    print(f"Exiting process...")