import { MapPin, Calendar, Settings2 } from "lucide-react";
import { Button } from "../../components/button";

export function DestinationAndDateHeader(){
    return(
        <div className="bg-zinc-900 px-4 h-16 rounded-xl shadow-shape flex items-center justify-between">
            <div className="flex items-center gap-2">
                <MapPin className="text-zinc-400 size-5" />
                <span>Florianópolis, Brasil</span>
            </div>

            <div className="flex items-center gap-5">
                <div className="flex items-center gap-2">
                    <Calendar className="text-zinc-400 size-5" />
                    <span>17 a 23 de Agosto</span>

                    <div className="w-px h-6 bg-zinc-800"/>

                    <Button variant="secondary">
                        Alterar local/data
                        <Settings2 className="size-5"/>
                    </Button>
                </div>
            </div>
        </div>
    )
}