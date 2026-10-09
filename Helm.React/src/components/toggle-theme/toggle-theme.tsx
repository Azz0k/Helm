import {Button} from "@/components/ui/button.tsx";
import {MoonIcon, SunIcon} from "lucide-react";


export const ToggleTheme = () => {
  return (
    <>
    <Button
      variant="outline"
      size="icon"
      aria-label="Toggle theme"
      title="Toggle theme"

    >
    <SunIcon className="rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0" />
    <MoonIcon className="absolute rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100" />
    <span className="sr-only">Toggle theme</span>
    </Button>

</>
  );
}