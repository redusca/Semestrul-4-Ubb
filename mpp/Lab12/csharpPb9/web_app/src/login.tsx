import { useState } from 'react';
import type { Arbitru } from './proba';

interface LoginProps {
    onLoginSuccess: (user: Arbitru) => void;
}

function Login({ onLoginSuccess }: LoginProps) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    async function handleLogin() {
        if (!username || !password) {
            alert('Please enter both username and password.');
            return;
        }

        setIsLoading(true);

        const ArbitruDTO = {
            Nume: username,
            Parola: password
        };

        try {
            const response = await fetch('https://localhost:7063/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(ArbitruDTO),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok: ' + response.statusText);
            }

            const data = await response.json();
            const user = data.user as Arbitru;

            if (user) {
                localStorage.setItem('token', JSON.stringify(data.token));
                localStorage.setItem('user', JSON.stringify(user));
                alert('Login successful, ' + user.nume + '!');
                
                // Call the success callback to update the parent component
                onLoginSuccess(user);
            } else {
                alert('Invalid username or password.');
            }
        } catch (error) {
            console.error('Error during login:', error);
            alert('Login failed. Please try again.');
        } finally {
            setIsLoading(false);
        }
    }

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        handleLogin();
    };

    return (
        <div className="login-container">
            <form onSubmit={handleSubmit} className="login-form">
                <h2>Login</h2>
                <div className="input-group">
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        disabled={isLoading}
                        required
                    />
                </div>
                <div className="input-group">
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        disabled={isLoading}
                        required
                    />
                </div>
                <button 
                    type="submit" 
                    disabled={isLoading || !username || !password}
                    className="login-btn"
                >
                    {isLoading ? 'Logging in...' : 'Login'}
                </button>
            </form>
        </div>
    );
}

export default Login;