import Vue from 'vue'
import Router from 'vue-router'
import store from "@/store"

import Login from '@/views/Login'
import Admin from '@/views/Admin'

import DefaultLayout from '@/layouts/DefaultLayout.vue'
import TeamLayout from '@/layouts/TeamLayout.vue'
import TechLayout from '@/layouts/TechLayout.vue'

import Rules from '@/views/main/Rules'
import Questions from '@/views/main/Questions'
import Map from '@/views/main/Map'
import Finish from '@/views/main/Finish'

import Home from '@/views/tech/Home'

import TeamIndex from '@/views/tech/team/Index'
import TeamCreate from '@/views/tech/team/Create'
import TeamEdit from '@/views/tech/team/Edit'

import QuestIndex from '@/views/tech/quest/Index'
import QuestCreate from '@/views/tech/quest/Create'
import QuestEdit from '@/views/tech/quest/Edit'

import QuestionGroupIndex from '@/views/tech/questionGroup/Index'
import QuestionGroupCreate from '@/views/tech/questionGroup/Create'
import QuestionGroupEdit from '@/views/tech/questionGroup/Edit'

import QuestionIndex from '@/views/tech/question/Index'
import QuestionCreate from '@/views/tech/question/Create'
import QuestionEdit from '@/views/tech/question/Edit'

import TeamAnswerIndex from '@/views/tech/teamAnswer/Index'
import TeamAnswerDetails from '@/views/tech/teamAnswer/Details'

import ServerImageIndex from '@/views/tech/serverImage/Index'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      redirect: function () {
        if (store.getters.accessToken) {
          if (store.getters.userType == "tech") {
            return '/tech/home'
          }
          else {
            return '/main/rules'
          }
        }
        else {
          return '/login'
        }
      },
      component: DefaultLayout,
      children: [
        {
          path: 'login',
          component: Login
        },
        {
          path: 'admin',
          component: Admin
        }
      ]
    },
    // main
    {
      path: '/main',
      component: TeamLayout,
      beforeEnter: function (to, from, next) {
        if (store.getters.userType != "team") {
          next('/login')
        }
        else {
          next()
        }
      },
      children: [
        {
          path: 'rules',
          component: Rules,
          beforeEnter: function (to, from, next) {
            if (store.getters.teamStatus == "finished") {
              next('/main/finish')
            }
            else {
              next()
            }
          }
        },
        {
          path: 'questions',
          component: Questions,
          beforeEnter: function (to, from, next) {
            if (store.getters.teamStatus == "finished") {
              next('/main/finish')
            }
            else {
              next()
            }
          }
        },
        {
          path: 'map',
          component: Map,
          beforeEnter: function (to, from, next) {
            if (store.getters.teamStatus == "finished") {
              next('/main/finish')
            }
            else {
              next()
            }
          }
        },
        {
          path: 'finish',
          component: Finish,
          beforeEnter: function (to, from, next) {
            if (store.getters.teamStatus != "finished") {
              next('/main/rules')
            }
            else {
              next()
            }
          }
        }
      ]
    },
    // tech
    {
      path: '/tech',
      component: TechLayout,
      beforeEnter: function (to, from, next) {
        if (store.getters.userType != "tech") {
          next('/login')
        }
        else {
          next()
        }
      },
      children: [
        // tech/home
        {
          path: 'home',
          component: Home
        },
        // tech/team
        {
          path: 'team/index',
          component: TeamIndex
        },
        {
          path: 'team/create',
          component: TeamCreate
        },
        {
          path: 'team/edit/:id',
          component: TeamEdit
        },
        // tech/quest
        {
          path: 'quest/index',
          component: QuestIndex
        },
        {
          path: 'quest/create',
          component: QuestCreate
        },
        {
          path: 'quest/edit/:id',
          component: QuestEdit
        },
        // tech/questionGroup
        {
          path: 'questionGroup/index',
          component: QuestionGroupIndex
        },
        {
          path: 'questionGroup/create',
          component: QuestionGroupCreate
        },
        {
          path: 'questionGroup/edit/:id',
          component: QuestionGroupEdit
        },
        // tech/question
        {
          path: 'question/index',
          component: QuestionIndex
        },
        {
          path: 'question/create',
          component: QuestionCreate
        },
        {
          path: 'question/edit/:id',
          component: QuestionEdit
        },
        // tech/teamAnswer
        {
          path: 'teamAnswer/index',
          component: TeamAnswerIndex
        },
        {
          path: 'teamAnswer/details/:id',
          component: TeamAnswerDetails
        },
        // tech/serverImage
        {
          path: 'serverImage/index',
          component: ServerImageIndex
        }
      ]
    }
  ]
})
