export const environment = {
  production: false,
  baseUrl: 'http://localhost:5129',
  apiUrls: {
    food: {
      getLogs: '/api/food/user/{userId}',
      addLog: '/api/food',
    },
    goals: {
      getGoals: '/api/goals/user/{userId}',
      addGoal: '/api/goals',
      getProgress: '/api/goals/user/{userId}/progress',
      getCustomGoals: '/api/goals/user/{userId}/custom-goals',
      addCustomGoal: '/api/goals/custom-goal',
    },
    notifications: {
      getNotifications: '/api/notifications/user/{userId}',
      markAsRead: '/api/notifications/mark-as-read/{notificationId}',
    },
  },
};
