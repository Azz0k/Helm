import {createRootRoute, createRoute, createRouter} from "@tanstack/react-router";
import {NotFoundPage} from "@/pages/not-found/not-found.tsx";
import {WelcomePage} from "@/pages/welcome/welcome.tsx";
import {DefaultLayout} from "@/layouts/default-layout.tsx";


//const rootRoute = createRootRoute();
const rootRoute = createRootRoute({
  component: DefaultLayout ,
  notFoundComponent: NotFoundPage
})
const dashboardRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "/",
  component: ()=><WelcomePage />,
});
const helpCenterRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "/help-center",
  component: ()=><WelcomePage />,
});
const appsRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/apps',
  component: ()=><WelcomePage />,
});
const usersRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "/users",
  component: ()=><WelcomePage />,
});
const routeTree = rootRoute.addChildren(
  [
    dashboardRoute,
    usersRoute,
    helpCenterRoute,
    appsRoute
  ]);
const defaultNotFoundComponent = ()=> <NotFoundPage/>;
export const router = createRouter({routeTree, defaultNotFoundComponent});