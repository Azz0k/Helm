import type {NavGroup} from "@/components/app-sidebar/app-sidebar.types.ts";
import {ref} from "vue";
import { BadgeHelpIcon, BoxesIcon, LayoutDashboardIcon, UsersIcon } from '@lucide/vue'


export function useSidebar() {
    const navData = ref<NavGroup[]> ([
        {
            title: 'General',
            items: [
                { title: 'Dashboard', url: '/', icon: LayoutDashboardIcon },
                { title: 'Help Center', url: '/help-center', icon: BadgeHelpIcon },
                { title: 'Apps', url: '/apps', icon: BoxesIcon },
                { title: 'Users', url: '/users', icon: UsersIcon },

            ],
        }]);
    return {
        navData,
    };
}