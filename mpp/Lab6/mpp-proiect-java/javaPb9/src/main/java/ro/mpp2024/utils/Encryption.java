package ro.mpp2024.utils;

import java.util.Base64;

public class Encryption {
    public static String code(String password){
        return Base64.getEncoder().encodeToString(password.getBytes());
    }
}
