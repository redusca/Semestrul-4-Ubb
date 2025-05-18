package ro.mpp2024;

public class ManageException extends RuntimeException {
    public ManageException() {}
    public ManageException(String message) {
        super(message);
    }
    public ManageException(String message, Throwable cause) {super(message, cause);}
}
