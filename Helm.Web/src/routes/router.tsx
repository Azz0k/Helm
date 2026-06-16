import {createRootRoute, createRoute, createRouter} from "@tanstack/react-router";
import {UsersContent} from "@/content/users/users-content.tsx";
import {HomeContent} from "@/content/home/home-content.tsx";
import {UserRolesContent} from "@/content/user-roles/user-roles-content.tsx";

const rootRoute = createRootRoute();
const indexRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/',
  component: ()=><HomeContent />,
});
const usersRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/users',
  component: ()=><UsersContent />,
});
const userRolessRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/userroles',
  component: ()=><UserRolesContent />,
});
const routeTree = rootRoute.addChildren(
  [
    indexRoute,
    usersRoute,
    userRolessRoute
  ]);
export const router = createRouter({routeTree});