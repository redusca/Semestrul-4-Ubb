package ro.mpp2024.networkUtils;

public class ServerException extends Exception{
  public ServerException() {
    super();
  }

  public ServerException(String message) {
    super(message);
  }

  public ServerException(String message, Throwable cause) {
    super(message, cause);
  }
}
