using System;
using System.Collections.Generic;
using System.Linq;
using model;
using model.Enum;
using services;
using services.Interfaces;
using persistence.repo.Interface;
using repository;
using services.TestService;

namespace ServiceTests
{
    public class ServiceImplTests
    {
        private ServiceImpl _service;
        private MockBugRepository _bugRepository;
        private MockUserRepository _userRepository;
        private MockObserver _observer1;
        private MockObserver _observer2;

        public ServiceImplTests()
        {
            _bugRepository = new MockBugRepository();
            _userRepository = new MockUserRepository();
            _service = new ServiceImpl(_bugRepository, _userRepository);
            _observer1 = new MockObserver();
            _observer2 = new MockObserver();
        }

        public void RunAllTests()
        {
            Console.WriteLine("=== Starting ServiceImpl Tests ===\n");

            TestLogin();
            TestLoginFailures();
            TestLogout();
            TestCreateUser();
            TestCreateUserDuplicate();
            TestGetBugs();
            TestAddBugs();
            TestChangeBugStatus();
            TestObserverNotifications();
            TestMultipleClientsLogin();

            Console.WriteLine("\n=== All Tests Completed ===");
        }

        private void TestLogin()
        {
            Console.WriteLine("Testing Login...");
            try
            {
                var user = _service.Login("admin", "admin123", _observer1);
                Console.WriteLine($"✓ Login successful: {user.Username} ({user.Type})");
                _service.Logout(user); // Clean up for other tests
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ Login test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestLoginFailures()
        {
            Console.WriteLine("Testing Login Failures...");

            // Test wrong password
            try
            {
                _service.Login("admin", "wrongpassword", _observer1);
                Console.WriteLine("✗ Should have failed with wrong password");
            }
            catch (Exception)
            {
                Console.WriteLine("✓ Correctly rejected wrong password");
            }

            // Test non-existent user
            try
            {
                _service.Login("nonexistent", "password", _observer1);
                Console.WriteLine("✗ Should have failed with non-existent user");
            }
            catch (Exception)
            {
                Console.WriteLine("✓ Correctly rejected non-existent user");
            }

            // Test double login
            try
            {
                _service.Login("admin", "admin123", _observer1);
                _service.Login("admin", "admin123", _observer2);
                Console.WriteLine("✗ Should have failed with double login");
            }
            catch (Exception)
            {
                Console.WriteLine("✓ Correctly rejected double login");
                // Clean up
                try { _service.Logout(_userRepository.FindByUsername("admin")); } catch { }
            }
            Console.WriteLine();
        }

        private void TestLogout()
        {
            Console.WriteLine("Testing Logout...");
            try
            {
                var user = _service.Login("tester1", "test123", _observer1);
                _service.Logout(user);

                // Should be able to login again after logout
                user = _service.Login("tester1", "test123", _observer1);
                Console.WriteLine("✓ Logout successful - user can login again");
                _service.Logout(user);
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ Logout test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestCreateUser()
        {
            Console.WriteLine("Testing CreateUser...");
            try
            {
                _service.CreateUser("newuser", "newpass", UserType.programmer);
                var createdUser = _userRepository.FindByUsername("newuser");
                if (createdUser != null && createdUser.Type == UserType.programmer)
                {
                    Console.WriteLine("✓ User created successfully");
                }
                else
                {
                    Console.WriteLine("✗ User not created properly");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ CreateUser test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestCreateUserDuplicate()
        {
            Console.WriteLine("Testing CreateUser with duplicate username...");
            try
            {
                _service.CreateUser("admin", "newpass", UserType.tester);
                Console.WriteLine("✗ Should have failed with duplicate username");
            }
            catch (Exception)
            {
                Console.WriteLine("✓ Correctly rejected duplicate username");
            }
            Console.WriteLine();
        }

        private void TestGetBugs()
        {
            Console.WriteLine("Testing GetBugs...");
            try
            {
                var bugs = _service.GetBugs();
                Console.WriteLine($"✓ GetBugs returned {bugs.Count()} bugs");
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ GetBugs test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestAddBugs()
        {
            Console.WriteLine("Testing AddBugs...");
            try
            {
                // Login a user to receive notifications
                var user = _service.Login("programmer1", "prog123", _observer1);

                var newBugs = new List<Bug>
                {
                    new Bug("Critical bug in login system", BugStatus.UnSolved),
                    new Bug("UI not responsive on mobile", BugStatus.UnSolved),
                    new Bug("Database connection timeout", BugStatus.WorkingOn)
                };

                int initialCount = _service.GetBugs().Count();
                _service.AddBugs(newBugs);
                int finalCount = _service.GetBugs().Count();

                if (finalCount == initialCount + 3)
                {
                    Console.WriteLine("✓ AddBugs successful - 3 bugs added");
                }
                else
                {
                    Console.WriteLine($"✗ AddBugs failed - expected {initialCount + 3}, got {finalCount}");
                }

                _service.Logout(user);
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ AddBugs test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestChangeBugStatus()
        {
            Console.WriteLine("Testing ChangeBugStatus...");
            try
            {
                // Login a user to receive notifications
                var user = _service.Login("tester1", "test123", _observer2);

                var bugs = _service.GetBugs().ToList();
                if (bugs.Any())
                {
                    var bugToUpdate = bugs.First();
                    var originalStatus = bugToUpdate.Status;
                    var newStatus = originalStatus == BugStatus.UnSolved ? BugStatus.WorkingOn : BugStatus.Solved;

                    bugToUpdate.Status = newStatus;
                    _service.ChangeBugStatus(bugToUpdate);

                    var updatedBug = _bugRepository.FindByBugNo(bugToUpdate.BugNo);
                    if (updatedBug.Status == newStatus)
                    {
                        Console.WriteLine($"✓ Bug status changed from {originalStatus} to {newStatus}");
                    }
                    else
                    {
                        Console.WriteLine("✗ Bug status not updated correctly");
                    }
                }
                else
                {
                    Console.WriteLine("✗ No bugs available to test status change");
                }

                _service.Logout(user);
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ ChangeBugStatus test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestObserverNotifications()
        {
            Console.WriteLine("Testing Observer Notifications...");
            try
            {
                // Login two users
                var user1 = _service.Login("admin", "admin123", _observer1);
                var user2 = _service.Login("newuser", "newpass", _observer2);

                // Clear previous notifications
                _observer1.BugsAddedNotifications.Clear();
                _observer1.BugStatusChangedNotifications.Clear();
                _observer2.BugsAddedNotifications.Clear();
                _observer2.BugStatusChangedNotifications.Clear();

                // Test bug addition notifications
                var testBugs = new List<Bug> { new Bug("Notification test bug", BugStatus.UnSolved) };
                _service.AddBugs(testBugs);

                // Give a moment for async notifications
                System.Threading.Thread.Sleep(100);

                if (_observer1.BugsAddedNotifications.Count > 0 && _observer2.BugsAddedNotifications.Count > 0)
                {
                    Console.WriteLine("✓ Both observers received bug addition notifications");
                }
                else
                {
                    Console.WriteLine("✗ Not all observers received bug addition notifications");
                }

                // Test bug status change notifications
                var bugs = _service.GetBugs().ToList();
                if (bugs.Any())
                {
                    var bug = bugs.Last();
                    bug.Status = BugStatus.Solved;
                    _service.ChangeBugStatus(bug);

                    System.Threading.Thread.Sleep(100);

                    if (_observer1.BugStatusChangedNotifications.Count > 0 && _observer2.BugStatusChangedNotifications.Count > 0)
                    {
                        Console.WriteLine("✓ Both observers received bug status change notifications");
                    }
                    else
                    {
                        Console.WriteLine("✗ Not all observers received bug status change notifications");
                    }
                }

                _service.Logout(user1);
                _service.Logout(user2);
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ Observer notifications test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestMultipleClientsLogin()
        {
            Console.WriteLine("Testing Multiple Clients Login...");
            try
            {
                var user1 = _service.Login("admin", "admin123", _observer1);
                var user2 = _service.Login("tester1", "test123", _observer2);

                Console.WriteLine($"✓ Multiple users logged in: {user1.Username} and {user2.Username}");

                _service.Logout(user1);
                _service.Logout(user2);
            }
            catch (Exception e)
            {
                Console.WriteLine($"✗ Multiple clients login test failed: {e.Message}");
            }
            Console.WriteLine();
        }

        private void TestNonExistentBugStatusChange()
        {
            Console.WriteLine("Testing ChangeBugStatus with non-existent bug...");
            try
            {
                var nonExistentBug = new Bug(99999, "Non-existent bug", BugStatus.Solved);
                _service.ChangeBugStatus(nonExistentBug);
                Console.WriteLine("✗ Should have failed with non-existent bug");
            }
            catch (Exception)
            {
                Console.WriteLine("✓ Correctly rejected non-existent bug status change");
            }
            Console.WriteLine();
        }
    }
}