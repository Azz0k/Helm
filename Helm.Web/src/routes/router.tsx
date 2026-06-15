import {createRootRoute, createRoute, createRouter} from "@tanstack/react-router";
import {UsersContent} from "@/content/users/users-content.tsx";
import {HomeContent} from "@/content/home/home-content.tsx";

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
const routeTree = rootRoute.addChildren(
  [
    indexRoute,
    usersRoute,
  ]);
export const router = createRouter({routeTree});