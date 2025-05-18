import dto.ProbaDTO;
import org.junit.jupiter.api.*;
import org.springframework.http.*;
import org.springframework.web.client.RestTemplate;
import ro.mpp2024.model.Proba;

@TestMethodOrder(MethodOrderer.OrderAnnotation.class)
public class restclinttest {
    private static final String BASE_URL = "https://localhost:7063/api/proba";
    private static final RestTemplate restTemplate = new RestTemplate();

    @BeforeAll
    public static void setUp() {
        System.out.println("Setting up tests...");
        disableSslVerification();
    }

    @AfterAll
    public static void tearDown() {
        System.out.println("Done  tests");
    }

    @Test
    @Order(1)
    public void testPostAddProba() {
        ProbaDTO probaDTO = new ProbaDTO("Proba1", "ciclism");

        HttpHeaders header = new HttpHeaders();
        header.setContentType(MediaType.APPLICATION_JSON);
        HttpEntity<ProbaDTO> entity = new HttpEntity<>(probaDTO, header);

        ResponseEntity<String> response = restTemplate.postForEntity(BASE_URL, probaDTO, String.class);

        assert(response.getStatusCode() == HttpStatus.CREATED);
        assert(response.getBody() != null);

        System.out.println("Response body ADD: " + response.getBody());
    }

    @Test
    @Order(2)
    public void testGetProba() {
        String id = "c4";
        ResponseEntity<String> response = restTemplate.getForEntity(BASE_URL + "/" + id, String.class);

        assert(response.getStatusCode() == HttpStatus.OK);
        assert(response.getBody() != null);

        System.out.println("Response body GET: " + response.getBody());
    }

    @Test
    @Order(4)
    public void testDeleteProba() {
        String id = "c4";
        HttpHeaders header = new HttpHeaders();
        header.setContentType(MediaType.APPLICATION_JSON);
        HttpEntity<ProbaDTO> entity = new HttpEntity<>(header);

        ResponseEntity<String> response = restTemplate.exchange(BASE_URL + "/" + id, HttpMethod.DELETE, entity, String.class);
        assert(response.getStatusCode() == HttpStatus.OK);
        assert(response.getBody() != null);

        System.out.println("Response body DELETE: " + response.getBody());
    }

    @Test
    @Order(3)
    public void testUpdateProba() {
        String id = "c4";
        ProbaDTO probaDTO = new ProbaDTO("Proba1Uptdated", "ciclism");

        HttpHeaders header = new HttpHeaders();
        header.setContentType(MediaType.APPLICATION_JSON);
        HttpEntity<ProbaDTO> entity = new HttpEntity<>(probaDTO, header);

        ResponseEntity<String> response = restTemplate.exchange(BASE_URL + "/" + id, HttpMethod.PUT, entity, String.class);
        assert(response.getStatusCode() == HttpStatus.OK);
        assert(response.getBody() != null);

        System.out.println("Response body UPDATE: " + response.getBody());
    }

    @Test
    @Order(5)
    public void testGetAllProba() {
        ResponseEntity<String> response = restTemplate.getForEntity(BASE_URL, String.class);
        assert(response.getStatusCode() == HttpStatus.OK);
        assert(response.getBody() != null);

        System.out.println("Response body GETALL: " + response.getBody());
    }

    private static void disableSslVerification() {
        try {
            javax.net.ssl.TrustManager[] trustAllCerts = new javax.net.ssl.TrustManager[]{
                    new javax.net.ssl.X509TrustManager() {
                        public java.security.cert.X509Certificate[] getAcceptedIssuers() {
                            return null;
                        }
                        public void checkClientTrusted(java.security.cert.X509Certificate[] certs, String authType) {
                        }
                        public void checkServerTrusted(java.security.cert.X509Certificate[] certs, String authType) {
                        }
                    }
            };

            javax.net.ssl.SSLContext sc = javax.net.ssl.SSLContext.getInstance("TLS");
            sc.init(null, trustAllCerts, new java.security.SecureRandom());
            javax.net.ssl.HttpsURLConnection.setDefaultSSLSocketFactory(sc.getSocketFactory());
            javax.net.ssl.HttpsURLConnection.setDefaultHostnameVerifier((hostname, session) -> true);
        } catch (Exception e) {
            throw new RuntimeException("Failed to disable SSL verification", e);
        }
    }
}
