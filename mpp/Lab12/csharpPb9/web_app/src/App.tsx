import { useCallback, useState, useEffect } from 'react'
import './App.css'
import RightPanel from './RightPanel'
import type { Proba, Arbitru } from './proba'
import LeftPanel from './LeftPanel'
import Login from './login'

function App() {
    const [probaList, setProbaList] = useState<Proba[]>([])
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false)
    const [currentUser, setCurrentUser] = useState<Arbitru | null>(null)

    // Check if user is already logged in on app start
    useEffect(() => {
        const token = localStorage.getItem('token')
        const user = localStorage.getItem('user')
        
        if (token && user) {
            try {
                setCurrentUser(JSON.parse(user))
                setIsLoggedIn(true)
            } catch (error) {
                console.error('Error parsing stored user data:', error)
                // Clear invalid data
                localStorage.removeItem('token')
                localStorage.removeItem('user')
            }
        }
    }, [])

    const getAllProba = useCallback(() => {
        const token = localStorage.getItem('token')
        if (!token) {
            console.error('No token available')
            return
        }

        fetch('https://localhost:7063/api/proba', {
            headers: {
                'Authorization': `Bearer ${JSON.parse(token)}`,
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok')
                }
                return response.json()
            })
            .then(data => {
                setProbaList(data)
            })
            .catch(error => {
                console.error('Error fetching proba:', error)
            })
    }, [])

    const handleLoginSuccess = (user: Arbitru) => {
        setCurrentUser(user)
        setIsLoggedIn(true)
    }

    const handleLogout = () => {
        localStorage.removeItem('token')
        localStorage.removeItem('user')
        setCurrentUser(null)
        setIsLoggedIn(false)
        setProbaList([])
    }

    if (!isLoggedIn) {
        return <Login onLoginSuccess={handleLoginSuccess} />
    }

    return (
        <div className="app-container">
            <header className="app-header">
                <h1>Proba Management System</h1>
                <div className="user-info">
                    <span>Welcome, {currentUser?.nume}!</span>
                    <button onClick={handleLogout} className="logout-btn">Logout</button>
                </div>
            </header>
            <div className="main-content">
                <LeftPanel loadList={getAllProba} />
                <RightPanel 
                    probaList={probaList} 
                    setProbaList={setProbaList} 
                    loadList={getAllProba} 
                />
            </div>
        </div>
    )
}

export default App