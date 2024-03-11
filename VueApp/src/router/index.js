// Composables
import { createRouter, createWebHistory } from 'vue-router'
import CalculatorList from '@/components/CalculatorList.vue'
import BMI from '@/components/calculators/BMI.vue'
import WHR from '@/components/calculators/WHR.vue'
import PPMCPM from '@/components/calculators/PPMCPM.vue'
import WeightPlanner from '@/components/calculators/WeightPlanner.vue'
import PhisicalActivity from '@/components/calculators/PhisicalActivity.vue'
import FoodCalories from '@/components/calculators/FoodCalories.vue'
import LogIn from '@/components/LogIn.vue'
import SignUp from '@/components/SignUp.vue'
import Report from '@/components/Report.vue'
import UserProfile from '@/components/UserProfile.vue'
import Blog from '@/components/Blog.vue'
import BlogPost from '@/components/BlogPost.vue'
import CreateBlogPost from '@/components/CreateBlogPost.vue'

const routes = [
  {
    path: '/',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: '',
        name: 'Home',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "home" */ '@/components/Home.vue'),
      },
      {
        path: '/CalculatorList', name: 'Calculators', component: CalculatorList
        },
        {
            path: '/CalculatorList/BMI', name: 'BMI', component: BMI
        },
        {
            path: '/CalculatorList/WHR', name: 'WHR', component: WHR
        },
        {
            path: '/CalculatorList/PPMCPM', name: 'PPMCPM', component: PPMCPM
        },
        {
            path: '/CalculatorList/WeightPlanner', name: 'WeightPlanner', component: WeightPlanner
        },
        {
            path: '/CalculatorList/PhisicalActivity', name: 'PhisicalActivity', component: PhisicalActivity
        },
        {
            path: '/CalculatorList/FoodCalories', name: 'FoodCalories', component: FoodCalories
        },
        {
            path: '/LogIn', name: 'LogIn', component: LogIn
        },
        {
            path: '/SignUp', name: 'SignUp', component: SignUp
        },
        {
            path: '/Report', name: 'Report', component: Report
        },
        {
            path: '/UserProfile', name: 'UserProfile', component: UserProfile
        },
        {
            path: '/Blog', name: 'Blog', component: Blog
        },
        {
            path: '/Blog/BlogPost/:postId', name: 'BlogPost', component: BlogPost
        },
        {
            path: '/Blog/CreateBlogPost', name: 'CreateBlogPost', component: CreateBlogPost
        },
    ],
  },
]

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

export default router
